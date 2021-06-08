using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data.Common;
using TrueNorthChallenge.DBEntities;

namespace TrueNorthChallenge.DAL
{
    public class TrueNorthContext : DbContext
    {
        public TrueNorthContext(DbContextOptions<TrueNorthContext> options) : base(options) { }

        ~TrueNorthContext()
        {
            Dispose();
        }

        private IDbContextTransaction dbTran;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Posts_Tags>()
                .HasOne(bc => bc.Post)
                .WithMany(b => b.Posts_Tags)
                .HasForeignKey(bc => bc.PostId);
            modelBuilder.Entity<Posts_Tags>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.Posts_Tags)
                .HasForeignKey(bc => bc.TagId);
        }

        public void InitDBTransaction(System.Data.IsolationLevel il)
        {
            dbTran = Database.BeginTransaction(il);
        }

        public void CommitDBTransaction()
        {
            if (dbTran != null)
            {
                dbTran.Commit();
                dbTran = null;
            }
            else
            {
                throw new ApplicationException("Transacción no iniciada");
            }
        }

        public void RollbackDBTransaction()
        {
            if (dbTran != null)
            {
                dbTran.Rollback();
                dbTran = null;
            }
            else
            {
                throw new ApplicationException("Transacción no iniciada");
            }
        }

        public DbTransaction UnderlyingTransaction
        {
            get
            {
                if (dbTran != null)
                {
                    //return dbTran.UnderlyingTransaction;
                    return dbTran.GetDbTransaction();
                }
                else
                    return null;
            }
        }

        public bool IsInTransaction
        {
            get { return UnderlyingTransaction == null ? false : true; }
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public void Commit()
        {
            SaveChanges();
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            if (dbTran != null)
            {
                //Pongo try and catch porque no puedo modificar el timeout en tiempos de ejecución
                try
                {
                    dbTran.Rollback();
                    dbTran.Dispose();
                }
                catch { }
            }
            if (this != null)
            {
                base.Dispose();
            }
        }

        public DbConnection GetConnection()
        {
            return Database.GetDbConnection();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
