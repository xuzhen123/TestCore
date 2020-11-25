using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using XZ.Infrastructure;
using XZ.Main.Domain;

namespace XZ.Main.Repository
{
    public class MainContext : EFContext
    {
        public MainContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus) { }

        public DbSet<Contract> Contract { get; set; }
        public DbSet<Merchant> Merchant { get; set; }
        public DbSet<MerchantUser> MerchantUser { get; set; }
        public DbSet<SysUser> SysUser { get; set; }
        public DbSet<SysUserAndRole> SysUserAndRole { get; set; }
        public DbSet<SysRole> SysRole { get; set; }
        public DbSet<SysOperation> SysOperation { get; set; }
        public DbSet<SysPermission> SysPermission { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            //modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
