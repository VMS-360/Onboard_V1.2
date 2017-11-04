namespace Onboard.Web.UI.Models.HRViewModels
{
    public class ChecklistViewModel
    {
        public int Id { get; set; }

        public bool IsChecked { get; set; }

        public string Text { get; set; }

        public string CommentType { get; set; }

        public string CommentValue { get; set; }

        public string Helper { get; set; }
    }
}
