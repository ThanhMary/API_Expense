using Microsoft.EntityFrameworkCore;
using SocleRH_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.Data
{

        public class ExpenseApiContext : DbContext
        {

            public ExpenseApiContext(DbContextOptions<ExpenseApiContext> options) : base(options)
            {

            }
            public virtual DbSet<Expense> Expenses { get; set; }
            public virtual DbSet<Currency> Currencies { get; set; }
            public virtual DbSet<User> Users { get; set; }

            //protected override void OnModelCreating(ModelBuilder modelBuilder)
            //{

                //modelBuilder.Entity<User>()
                //      .HasOne<Currency>(s => s.Currency)
                //      .WithMany(s => s.Users)
                //      .HasForeignKey(s => s.CurrentCurrencyId);

                //modelBuilder.Entity<Currency>()
                //        .HasMany<User>(g => g.Users)
                //        .WithOne(s => s.Currency)
                //        .HasForeignKey(s => s.CurrentCurrencyId)
                //        .OnDelete(DeleteBehavior.Cascade);

                //modelBuilder.Entity<Currency>()
                //     .HasMany<Expense>(g => g.Expenses)
                //     .WithOne(s => s.Currency)
                //     .HasForeignKey(s => s.CurrentCurrencyId)
                //     .OnDelete(DeleteBehavior.Cascade);


                //modelBuilder.Entity<Expense>()
                //      .HasOne<User>(s => s.User)
                //      .WithMany(s => s.Expenses)
                //      .HasForeignKey(s => s.CurrentUserId)
                //      .OnDelete(DeleteBehavior.NoAction);

                //modelBuilder.Entity<Expense>()
                //  .HasOne<Currency>(s => s.Currency)
                //  .WithMany(s => s.Expenses)
                //  .HasForeignKey(s => s.CurrentCurrencyId)
                //  .OnDelete(DeleteBehavior.NoAction);

            //}

        }
    }
