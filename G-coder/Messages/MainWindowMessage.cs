using G_coder.Model;

namespace G_coder.Messages
{
    class MainWindowMessage
    {
        public string FileName { get; set; }
        public Fields Fields { get; set; }

        public MainWindowMessage(string fileName, Fields fields)
        {
            FileName = fileName;
            Fields = fields;
        }
    }
}
