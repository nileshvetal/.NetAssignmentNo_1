namespace logtocsvconverter
{
    public class CSVFormat
    {
        public CSVFormat()
        {
            this.Level = "";
            this.Date = "";
            this.Time = "";
            this.Text = "";
            this.Number = 0;
        }

        public int Number {get; set;}

        public string Level { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Text { get; set; }

    }
}