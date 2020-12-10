using DatabaseOracleContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelContext;
using System;
using System.Reflection;

namespace ServiceContext.Provider.Oracle
{
    public class RepositoryDbOracleContext : BaseDbContext
    {
        private readonly string _connString;
        private readonly IConfiguration _config;
        public RepositoryDbOracleContext(string connstring, IConfiguration config) : base(connstring)
        {
            _connString = connstring;
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            string ContextAssemblyName = "ModelContext";
            var AssemblyModelConfig = _config["AssemblyRegister"];
            if (AssemblyModelConfig != null)
                ContextAssemblyName = AssemblyModelConfig.ToString();
            Assembly modelInAssembly = Assembly.Load(new AssemblyName(ContextAssemblyName));
            var exportedTypes = modelInAssembly.ExportedTypes;
            foreach (Type type in exportedTypes)
            {
                if (type.IsClass)
                {
                    var method = builder.GetType().GetMethod("Entity", new Type[] { });
                    method = method.MakeGenericMethod(new Type[] { type });
                    method.Invoke(builder, null);
                }
            }
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            int timeout = 10;
            var ConfigTimeOutConnection = _config["TimeOutDB"];
            if (ConfigTimeOutConnection != null)
            {
                timeout = Convert.ToInt32(ConfigTimeOutConnection.ToString());
            }
            optionsBuilder.UseOracle(_connString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(timeout).TotalSeconds));
        }

        #region Register Outside Model in DBContext
        public DbSet<AttributeActionResult> AttributeActionResult { get; set; }
        #endregion
    }
}
