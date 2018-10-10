using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace DragAndDrop.Controls
{
    internal class NumericTextBox : TextBox
    {
        public NumericTextBox()
        {
            this.PreviewTextInput += this.NumericTextBox_PreviewTextInput;
            DataObject.AddPastingHandler(this, this.TextBoxPastingEventHandler);
            this.GotFocus += (sender, e) => this.SelectAll();
        }

        private void NumericTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "[0-9]+");
        }

        private void TextBoxPastingEventHandler(object sender, DataObjectPastingEventArgs e)
        {
            var value = e.DataObject.GetData(typeof(string)) as string;

            var isNumeric = Regex.IsMatch(value, "[0-9]+");
            if (!isNumeric)
            {
                e.CancelCommand();
            }
            e.Handled = !isNumeric;
        }
    }
}
