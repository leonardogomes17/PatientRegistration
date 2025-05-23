using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientRegistration.Data.Migrations
{
    /// <inheritdoc />
    /// 
    
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250522034153_AddAgreementsRegisters")]
    public partial class AddAgreementsRegisters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"insert into [dbo].[Agreement] ( AgreementName ) values ('Amil')");
            migrationBuilder.Sql(@"insert into [dbo].[Agreement] ( AgreementName ) values ('Bradesco')");
            migrationBuilder.Sql(@"insert into [dbo].[Agreement] ( AgreementName ) values ('NotreDame')");
            migrationBuilder.Sql(@"insert into [dbo].[Agreement] ( AgreementName ) values ('Porto Seguro')");
            migrationBuilder.Sql(@"insert into [dbo].[Agreement] ( AgreementName ) values ('SulAmérica')");
            migrationBuilder.Sql(@"insert into [dbo].[Agreement] ( AgreementName ) values ('Unimed')");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
