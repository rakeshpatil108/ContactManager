using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ContactDetail
    {
        public int ID { get; set; }

        [Required(ErrorMessage =MessageConstant.FirstNameRequired)]
        [MinLength(2,ErrorMessage =MessageConstant.FirstNameLength)]
        [MaxLength(10, ErrorMessage = MessageConstant.FirstNameLength)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = MessageConstant.LastNameRequired)]
        [MinLength(2, ErrorMessage = MessageConstant.LastNameLength)]
        [MaxLength(10, ErrorMessage = MessageConstant.LastNameLength)]
        public string LastName { get; set; }

        [Required(ErrorMessage = MessageConstant.EmailRequired)]
        [EmailAddress(ErrorMessage =MessageConstant.EmailInavaid)]
        public string Email { get; set; }

        [Required(ErrorMessage = MessageConstant.PhoneNumberRequired)]
        [RegularExpression(@"[0-9]{10}$",ErrorMessage =MessageConstant.PhoneNumberInvalid)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = MessageConstant.StatusRequired)]
        [EnumDataType(typeof(ContactStatus),ErrorMessage =MessageConstant.StatusInvalid)]
        public ContactStatus Status { get; set; }
    }
    public enum ContactStatus
    {
        Active,
        Inactive
    }
}
