namespace FreeCourse.Web.Models.Catalog
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Decimal Price { get; set; }

        public string Picture { get; set; }

        public string UserId { get; set; }

        public FeatureViewModel? Feature { get; set; }

        public string? CategoryId { get; set; }
    }
}
