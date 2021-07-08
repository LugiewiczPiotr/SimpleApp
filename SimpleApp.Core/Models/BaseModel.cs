using System;

namespace SimpleApp.Core.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }

        public BaseModel()
        {
            Id = Guid.NewGuid();
            IsActive = true;

        }
    }
}
