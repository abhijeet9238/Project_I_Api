using System;

namespace Project_I.Model
{
    public class ProjectModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

    }
    public class ConfirmProjectModel
    {
        public string Id { get; set; }
        public ProjectStatus Status { get; set; }
    }
    public enum ProjectStatus
    {
        Pending=1,
        Active=2
    }
}
