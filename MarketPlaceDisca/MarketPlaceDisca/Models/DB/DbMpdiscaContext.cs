using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MarketPlaceDisca.Models.DB;

public partial class DbMpdiscaContext : DbContext
{
    public DbMpdiscaContext()
    {
    }

    public DbMpdiscaContext(DbContextOptions<DbMpdiscaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Credential> Credentials { get; set; }

    public virtual DbSet<CredentialsLog> CredentialsLogs { get; set; }

    public virtual DbSet<Departament> Departaments { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceHasCategory> ServiceHasCategories { get; set; }

    public virtual DbSet<ServicesContract> ServicesContracts { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=db_mpdisca;uid=root;pwd=7611", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Idcategory).HasName("PRIMARY");

            entity
                .ToTable("category")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            entity.Property(e => e.Idcategory)
                .ValueGeneratedNever()
                .HasColumnName("idcategory");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .HasColumnName("description");
            entity.Property(e => e.NameCategory)
                .HasMaxLength(45)
                .HasColumnName("name_category");
        });

        modelBuilder.Entity<Credential>(entity =>
        {
            entity.HasKey(e => e.IdCredentials).HasName("PRIMARY");

            entity.ToTable("credentials");

            entity.HasIndex(e => e.UserIdUser, "fk_Credentials_User1_idx");

            entity.Property(e => e.IdCredentials).HasColumnName("idCredentials");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.UserIdUser)
                .HasMaxLength(20)
                .HasColumnName("User_idUser");
            entity.Property(e => e.UserUserTypeIduserType).HasColumnName("User_userType_iduserType");
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .HasColumnName("username");

            entity.HasOne(d => d.UserIdUserNavigation).WithMany(p => p.Credentials)
                .HasForeignKey(d => d.UserIdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Credentials_User1");
        });

        modelBuilder.Entity<CredentialsLog>(entity =>
        {
            entity.HasKey(e => e.IdCredentialsLog).HasName("PRIMARY");

            entity.ToTable("credentials_log");

            entity.HasIndex(e => new { e.UserIdUser, e.UserUserTypeIduserType }, "fk_Credentials_Log_User1_idx");

            entity.Property(e => e.IdCredentialsLog)
                .ValueGeneratedNever()
                .HasColumnName("idCredentials_Log");
            entity.Property(e => e.Date)
                .HasMaxLength(45)
                .HasColumnName("date");
            entity.Property(e => e.IpUpdater)
                .HasMaxLength(45)
                .HasColumnName("ip_updater");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.UserIdUser)
                .HasMaxLength(20)
                .HasColumnName("User_idUser");
            entity.Property(e => e.UserUserTypeIduserType).HasColumnName("User_userType_iduserType");
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .HasColumnName("username");

            entity.HasOne(d => d.UserIdUserNavigation).WithMany(p => p.CredentialsLogs)
                .HasForeignKey(d => d.UserIdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Credentials_Log_User1");
        });

        modelBuilder.Entity<Departament>(entity =>
        {
            entity.HasKey(e => e.Codigodepartamento).HasName("PRIMARY");

            entity
                .ToTable("departament")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            entity.Property(e => e.Codigodepartamento)
                .ValueGeneratedNever()
                .HasColumnName("codigodepartamento");
            entity.Property(e => e.Nombredepartamento)
                .HasMaxLength(45)
                .HasColumnName("nombredepartamento");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.Idmunicipios).HasName("PRIMARY");

            entity
                .ToTable("municipios")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            entity.Property(e => e.Idmunicipios).HasColumnName("idmunicipios");
            entity.Property(e => e.Codigodepartamento).HasColumnName("codigodepartamento");
            entity.Property(e => e.Nombremunicipio)
                .HasMaxLength(45)
                .HasColumnName("nombremunicipio");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => new { e.ServiceIdService, e.UserIdUser })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("request");

            entity.HasIndex(e => e.ServiceIdService, "fk_Service_has_User_Service1_idx");

            entity.HasIndex(e => e.ServicesContractIdservicesContract, "fk_Service_has_User_Services_Contract1_idx");

            entity.HasIndex(e => e.UserIdUser, "fk_Service_has_User_User1_idx");

            entity.Property(e => e.ServiceIdService).HasColumnName("Service_idService");
            entity.Property(e => e.UserIdUser)
                .HasMaxLength(45)
                .HasColumnName("User_idUser");
            entity.Property(e => e.ServicesContractIdservicesContract).HasColumnName("Services_Contract_idservices_contract");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .HasColumnName("status");

            entity.HasOne(d => d.ServiceIdServiceNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.ServiceIdService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Service_has_User_Service1");

            entity.HasOne(d => d.ServicesContractIdservicesContractNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.ServicesContractIdservicesContract)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Service_has_User_Services_Contract1");

            entity.HasOne(d => d.UserIdUserNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserIdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Service_has_User_User1");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("PRIMARY");

            entity.ToTable("service");

            entity.HasIndex(e => e.IdService, "idService_UNIQUE").IsUnique();

            entity.Property(e => e.IdService).HasColumnName("idService");
            entity.Property(e => e.Address)
                .HasMaxLength(45)
                .HasColumnName("address");
            entity.Property(e => e.Categoria)
                .HasMaxLength(45)
                .HasColumnName("categoria");
            entity.Property(e => e.DatesDispo)
                .HasMaxLength(100)
                .HasColumnName("dates_dispo");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.InitialPrice).HasColumnName("initial_price");
            entity.Property(e => e.NameService)
                .HasMaxLength(50)
                .HasColumnName("name_service");
            entity.Property(e => e.PathPhotos)
                .HasMaxLength(45)
                .HasColumnName("pathPhotos");
        });

        modelBuilder.Entity<ServiceHasCategory>(entity =>
        {
            entity.HasKey(e => new { e.ServiceIdService, e.CategoryIdcategory })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("service_has_category");

            entity.HasIndex(e => e.ServiceIdService, "fk_Service_has_category_Service1_idx");

            entity.HasIndex(e => e.CategoryIdcategory, "fk_Service_has_category_category1_idx");

            entity.Property(e => e.ServiceIdService)
                .ValueGeneratedOnAdd()
                .HasColumnName("Service_idService");
            entity.Property(e => e.CategoryIdcategory).HasColumnName("category_idcategory");

            entity.HasOne(d => d.ServiceIdServiceNavigation).WithMany(p => p.ServiceHasCategories)
                .HasForeignKey(d => d.ServiceIdService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Service_has_category_Service1");
        });

        modelBuilder.Entity<ServicesContract>(entity =>
        {
            entity.HasKey(e => e.IdservicesContract).HasName("PRIMARY");

            entity.ToTable("services_contract");

            entity.Property(e => e.IdservicesContract)
                .ValueGeneratedNever()
                .HasColumnName("idservices_contract");
            entity.Property(e => e.DateEnd).HasColumnName("dateEnd");
            entity.Property(e => e.DateInit).HasColumnName("dateInit");
            entity.Property(e => e.Estado)
                .HasMaxLength(45)
                .HasColumnName("estado");
            entity.Property(e => e.Price)
                .HasMaxLength(30)
                .HasColumnName("price");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.IdSession).HasName("PRIMARY");

            entity.ToTable("session");

            entity.HasIndex(e => e.UserIdUser, "fk_Session_User1_idx");

            entity.Property(e => e.IdSession)
                .ValueGeneratedNever()
                .HasColumnName("idSession");
            entity.Property(e => e.SessionEnded).HasColumnName("session_ended");
            entity.Property(e => e.SessionStarted).HasColumnName("session_started");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Token)
                .HasMaxLength(30)
                .HasColumnName("token");
            entity.Property(e => e.UserIdUser)
                .HasMaxLength(20)
                .HasColumnName("User_idUser");
            entity.Property(e => e.UserUserTypeIduserType).HasColumnName("User_userType_iduserType");

            entity.HasOne(d => d.UserIdUserNavigation).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserIdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Session_User1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.IdUser)
                .HasMaxLength(45)
                .HasColumnName("idUser");
            entity.Property(e => e.Address)
                .HasMaxLength(45)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate).HasColumnName("birthDate");
            entity.Property(e => e.CoverPhoto)
                .HasMaxLength(45)
                .HasColumnName("coverPhoto");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(45)
                .HasColumnName("gender");
            entity.Property(e => e.LastNameUser)
                .HasMaxLength(45)
                .HasColumnName("lastNameUser");
            entity.Property(e => e.NameUser)
                .HasMaxLength(45)
                .HasColumnName("nameUser");
            entity.Property(e => e.Photo)
                .HasMaxLength(45)
                .HasColumnName("photo");
            entity.Property(e => e.Telephone)
                .HasMaxLength(45)
                .HasColumnName("telephone");
            entity.Property(e => e.TypeDocument)
                .HasMaxLength(2)
                .HasColumnName("typeDocument");

            entity.HasMany(d => d.ServiceIdServices).WithMany(p => p.UserIdUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserHasService",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceIdService")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_User_has_Service_Service"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserIdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_User_has_Subasta_User1"),
                    j =>
                    {
                        j.HasKey("UserIdUser", "ServiceIdService")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("user_has_service");
                        j.HasIndex(new[] { "ServiceIdService" }, "fk_User_has_Subasta_Subasta1_idx");
                        j.HasIndex(new[] { "UserIdUser" }, "fk_User_has_Subasta_User1_idx");
                        j.IndexerProperty<string>("UserIdUser")
                            .HasMaxLength(20)
                            .HasColumnName("User_idUser");
                        j.IndexerProperty<int>("ServiceIdService").HasColumnName("Service_idService");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
