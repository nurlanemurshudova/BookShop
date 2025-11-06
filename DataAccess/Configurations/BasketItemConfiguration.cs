
using Core.DefaultValues;
using Entities.Concrete.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.ToTable("BasketItems");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityColumn(seed: DefaultConstantValue.DEFAULT_PRIMARY_INCREMENT_VALUE, increment: 1);

        builder.Property(x => x.Quantity)
            .IsRequired();


        builder.Property(x => x.PriceAtTimeOfAddition)
            .IsRequired()
            .HasColumnType("decimal(18,2)"); 

        builder
            .HasOne(bi => bi.Basket)
            .WithMany(b => b.Items)
            .HasForeignKey(bi => bi.BasketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(bi => bi.Book)
            .WithMany() 
            .HasForeignKey(bi => bi.BookId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}