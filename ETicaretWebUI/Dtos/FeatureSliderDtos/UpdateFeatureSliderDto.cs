namespace ETicaretWebUI.Dtos.FeatureSliderDtos
{
    public class UpdateFeatureSliderDto
    {
        public int FeatureSliderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
    }
}
