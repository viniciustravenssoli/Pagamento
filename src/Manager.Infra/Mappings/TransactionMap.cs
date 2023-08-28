using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infra.Mappings
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(x => x.Receiver)
                .WithMany(x => x.TransactionAsReciever)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(t => t.ReceiverId);

            builder.HasOne(x => x.Sender)
                .WithMany(x => x.TransactionAsSender)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(t => t.SenderId);
        }
    }
}