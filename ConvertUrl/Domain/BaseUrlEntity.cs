using System;

namespace ConvertUrlRepository.Domain
{
    public class BaseUrlEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ExpiredDate { get; set; } = DateTime.Now.AddDays(15);
    }
}
