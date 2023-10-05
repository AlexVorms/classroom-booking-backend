using System.ComponentModel.DataAnnotations;


namespace classroom_booking_backend.DAL.Enums
{
    public enum RoleEnum
    {
        [Display(Name = ApplicationRoleNames.Administrator)]
        Administrator,
        [Display(Name = ApplicationRoleNames.Student)]
        Student,
        [Display(Name = ApplicationRoleNames.Teacher)]
        Teacher
    }

    public class ApplicationRoleNames
    {
        public const string Administrator = "Administrator";
        public const string Student = "Student";
        public const string Teacher = "Teacher";
    }
    
}
