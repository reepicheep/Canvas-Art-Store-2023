using System.ComponentModel.DataAnnotations;

using static CanvasArtStore.Common.EntityValidationConstants.Curator;

namespace CanvasArtStore.Web.ViewModels.Curator
{
    public class BecomeCuratorFormModel
    {
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
