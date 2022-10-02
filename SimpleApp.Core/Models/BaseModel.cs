using System;

namespace SimpleApp.Core.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid();
            IsActive = true;
        }

        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
