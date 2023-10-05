using System.ComponentModel.DataAnnotations;

namespace classroom_booking_backend.DAL.Enums
{
    public enum BookingStatusEnum
    {
        [Display(Name = ApplicationStatusNames.New)]
        New,
        [Display(Name = ApplicationStatusNames.Approved)]
        Approved,
        [Display(Name = ApplicationStatusNames.RequireApprove)]
        RequireApprove,
        [Display(Name = ApplicationStatusNames.Canceled)]
        Canceled,
        [Display(Name = ApplicationStatusNames.Rejected)]
        Rejected
    }
    public class ApplicationStatusNames
    {
        public const string New = "NEW";
        public const string Approved = "APPROVED";
        public const string RequireApprove = "REQUIRE APPROVE";
        public const string Canceled = "CANCELED";
        public const string Rejected = "REJECTED";
    }
}
