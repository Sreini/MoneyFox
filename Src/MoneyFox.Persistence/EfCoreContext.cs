﻿using Microsoft.EntityFrameworkCore;
using System;
using MoneyFox.Domain.Entities;
using MoneyFox.DataLayer.Configurations;
using MoneyFox.Application.Interfaces;
using MoneyFox.Persistence.Configurations;

namespace MoneyFox.Persistence
{

    /// <summary>
    ///     Represents the data context of the application
    /// </summary>
    public class EfCoreContext : DbContext, IEfCoreContext
    {
        public EfCoreContext(DbContextOptions options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<RecurringPayment> RecurringPayments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PaymentTag> PaymentTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        ///     Called when the models are created.
        ///     Enables to configure advanced settings for the models.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ThrowIfNull(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTagConfiguration());
        }

        private static void ThrowIfNull(ModelBuilder modelBuilder) { if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));  }
    }
}
