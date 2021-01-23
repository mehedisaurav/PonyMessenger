using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyChat.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyChat.Configuration
{
    public class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages", "dbo");
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Name);
            builder.Property(e => e.Text).HasMaxLength(500);
            builder.Property(e => e.TextTime).HasDefaultValueSql("GetDate()");
            builder.HasOne(e => e.Receiver).WithMany(e => e.ReceiverMessages).HasForeignKey(e => e.ReceiverId);
        }
    }
}
