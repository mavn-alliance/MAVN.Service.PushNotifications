using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAVN.Service.PushNotifications.MsSqlRepositories.Entities
{
    public class NotificationMessage : EntityBase
    {
        [Required]
        [MaxLength(50)]
        [Column("customer_id")]
        public string CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("message_group_id")]
        public string MessageGroupId { get; set; }

        [Required]
        [Column("creation_timestamp")]
        public DateTime CreationTimestamp { get; set; }

        [Required]
        [MaxLength(4000)]
        [Column("custom_payload")]
        public string CustomPayload { get; set; }

        [Required]
        [MaxLength(10000)]
        [Column("message")]
        public string Message { get; set; }

        [Required]
        [Column("is_read")]
        public bool IsRead { get; set; }
    }
}
