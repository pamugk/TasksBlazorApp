using System.ComponentModel.DataAnnotations;

namespace TasksSpa.Model.Input
{
    public class TaskModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется ввести заголовок задачи")]
        [CustomValidation(typeof(Logic.InputValidator), "TaskTitleIsValid", 
            ErrorMessage = "Заголовок задачи содержит запрещённые символы")]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFinished { get; set; }
    }
}
