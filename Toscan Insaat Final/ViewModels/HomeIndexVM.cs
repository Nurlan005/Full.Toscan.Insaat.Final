using Toscan_Insaat_Final.Dtos.AboutDtos;
using Toscan_Insaat_Final.Dtos.ServiceDtos;
using Toscan_Insaat_Final.Dtos.SliderDto;

namespace Toscan_Insaat_Final.ViewModels
{
    public class HomeIndexVM
    {
        public List<ServiceDto> Services { get; set; }
        public AboutDto About { get; set; }
        public List<SliderDto> Slider { get; set; }
    }
}
