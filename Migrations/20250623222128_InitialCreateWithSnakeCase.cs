using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthRoleManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_role_claims_asp_net_roles_RoleId",
                table: "asp_net_role_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_claims_asp_net_users_UserId",
                table: "asp_net_user_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_logins_asp_net_users_UserId",
                table: "asp_net_user_logins");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_roles_asp_net_roles_RoleId",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_roles_asp_net_users_UserId",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_tokens_asp_net_users_UserId",
                table: "asp_net_user_tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_OpenIddictAuthorizations_OpenIddictApplications_Application~",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropForeignKey(
                name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                table: "OpenIddictTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                table: "OpenIddictTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_users_role_RoleId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role",
                table: "role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenIddictTokens",
                table: "OpenIddictTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenIddictScopes",
                table: "OpenIddictScopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenIddictAuthorizations",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpenIddictApplications",
                table: "OpenIddictApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_users",
                table: "asp_net_users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_user_tokens",
                table: "asp_net_user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_user_roles",
                table: "asp_net_user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_user_logins",
                table: "asp_net_user_logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_user_claims",
                table: "asp_net_user_claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_roles",
                table: "asp_net_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_role_claims",
                table: "asp_net_role_claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "user");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "role",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "role",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "role",
                newName: "update_date");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "role",
                newName: "create_date");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "OpenIddictTokens",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "OpenIddictTokens",
                newName: "subject");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "OpenIddictTokens",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Properties",
                table: "OpenIddictTokens",
                newName: "properties");

            migrationBuilder.RenameColumn(
                name: "Payload",
                table: "OpenIddictTokens",
                newName: "payload");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OpenIddictTokens",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ReferenceId",
                table: "OpenIddictTokens",
                newName: "reference_id");

            migrationBuilder.RenameColumn(
                name: "RedemptionDate",
                table: "OpenIddictTokens",
                newName: "redemption_date");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "OpenIddictTokens",
                newName: "expiration_date");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "OpenIddictTokens",
                newName: "creation_date");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyToken",
                table: "OpenIddictTokens",
                newName: "concurrency_token");

            migrationBuilder.RenameColumn(
                name: "AuthorizationId",
                table: "OpenIddictTokens",
                newName: "authorization_id");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "OpenIddictTokens",
                newName: "application_id");

            migrationBuilder.RenameIndex(
                name: "IX_OpenIddictTokens_ReferenceId",
                table: "OpenIddictTokens",
                newName: "ix_open_iddict_tokens_reference_id");

            migrationBuilder.RenameIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                table: "OpenIddictTokens",
                newName: "ix_open_iddict_tokens_authorization_id");

            migrationBuilder.RenameIndex(
                name: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
                table: "OpenIddictTokens",
                newName: "ix_open_iddict_tokens_application_id_status_subject_type");

            migrationBuilder.RenameColumn(
                name: "Resources",
                table: "OpenIddictScopes",
                newName: "resources");

            migrationBuilder.RenameColumn(
                name: "Properties",
                table: "OpenIddictScopes",
                newName: "properties");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "OpenIddictScopes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Descriptions",
                table: "OpenIddictScopes",
                newName: "descriptions");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "OpenIddictScopes",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OpenIddictScopes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DisplayNames",
                table: "OpenIddictScopes",
                newName: "display_names");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "OpenIddictScopes",
                newName: "display_name");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyToken",
                table: "OpenIddictScopes",
                newName: "concurrency_token");

            migrationBuilder.RenameIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes",
                newName: "ix_open_iddict_scopes_name");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "OpenIddictAuthorizations",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "OpenIddictAuthorizations",
                newName: "subject");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "OpenIddictAuthorizations",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Scopes",
                table: "OpenIddictAuthorizations",
                newName: "scopes");

            migrationBuilder.RenameColumn(
                name: "Properties",
                table: "OpenIddictAuthorizations",
                newName: "properties");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OpenIddictAuthorizations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "OpenIddictAuthorizations",
                newName: "creation_date");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyToken",
                table: "OpenIddictAuthorizations",
                newName: "concurrency_token");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "OpenIddictAuthorizations",
                newName: "application_id");

            migrationBuilder.RenameIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
                table: "OpenIddictAuthorizations",
                newName: "ix_open_iddict_authorizations_application_id_status_subject_type");

            migrationBuilder.RenameColumn(
                name: "Settings",
                table: "OpenIddictApplications",
                newName: "settings");

            migrationBuilder.RenameColumn(
                name: "Requirements",
                table: "OpenIddictApplications",
                newName: "requirements");

            migrationBuilder.RenameColumn(
                name: "Properties",
                table: "OpenIddictApplications",
                newName: "properties");

            migrationBuilder.RenameColumn(
                name: "Permissions",
                table: "OpenIddictApplications",
                newName: "permissions");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OpenIddictApplications",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RedirectUris",
                table: "OpenIddictApplications",
                newName: "redirect_uris");

            migrationBuilder.RenameColumn(
                name: "PostLogoutRedirectUris",
                table: "OpenIddictApplications",
                newName: "post_logout_redirect_uris");

            migrationBuilder.RenameColumn(
                name: "JsonWebKeySet",
                table: "OpenIddictApplications",
                newName: "json_web_key_set");

            migrationBuilder.RenameColumn(
                name: "DisplayNames",
                table: "OpenIddictApplications",
                newName: "display_names");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "OpenIddictApplications",
                newName: "display_name");

            migrationBuilder.RenameColumn(
                name: "ConsentType",
                table: "OpenIddictApplications",
                newName: "consent_type");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyToken",
                table: "OpenIddictApplications",
                newName: "concurrency_token");

            migrationBuilder.RenameColumn(
                name: "ClientType",
                table: "OpenIddictApplications",
                newName: "client_type");

            migrationBuilder.RenameColumn(
                name: "ClientSecret",
                table: "OpenIddictApplications",
                newName: "client_secret");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "OpenIddictApplications",
                newName: "client_id");

            migrationBuilder.RenameColumn(
                name: "ApplicationType",
                table: "OpenIddictApplications",
                newName: "application_type");

            migrationBuilder.RenameIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                newName: "ix_open_iddict_applications_client_id");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "asp_net_users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "asp_net_users",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "asp_net_users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "asp_net_users",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "asp_net_users",
                newName: "two_factor_enabled");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "asp_net_users",
                newName: "security_stamp");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "asp_net_users",
                newName: "phone_number_confirmed");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "asp_net_users",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "asp_net_users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                table: "asp_net_users",
                newName: "normalized_user_name");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                table: "asp_net_users",
                newName: "normalized_email");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                table: "asp_net_users",
                newName: "lockout_end");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                table: "asp_net_users",
                newName: "lockout_enabled");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "asp_net_users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "asp_net_users",
                newName: "email_confirmed");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "asp_net_users",
                newName: "concurrency_stamp");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "asp_net_users",
                newName: "access_failed_count");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "asp_net_user_tokens",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "asp_net_user_tokens",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "asp_net_user_tokens",
                newName: "login_provider");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "asp_net_user_tokens",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "asp_net_user_roles",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "asp_net_user_roles",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "asp_net_user_logins",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ProviderDisplayName",
                table: "asp_net_user_logins",
                newName: "provider_display_name");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                table: "asp_net_user_logins",
                newName: "provider_key");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "asp_net_user_logins",
                newName: "login_provider");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "asp_net_user_claims",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "asp_net_user_claims",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "asp_net_user_claims",
                newName: "claim_value");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "asp_net_user_claims",
                newName: "claim_type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "asp_net_roles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "asp_net_roles",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "asp_net_roles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "asp_net_roles",
                newName: "normalized_name");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "asp_net_roles",
                newName: "concurrency_stamp");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "asp_net_role_claims",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "asp_net_role_claims",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "asp_net_role_claims",
                newName: "claim_value");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "asp_net_role_claims",
                newName: "claim_type");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "user",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "user",
                newName: "update_date");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "user",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "user",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "user",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "user",
                newName: "create_date");

            migrationBuilder.RenameIndex(
                name: "IX_users_RoleId",
                table: "user",
                newName: "ix_user_role_id");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "asp_net_users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_role",
                table: "role",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_open_iddict_tokens",
                table: "OpenIddictTokens",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_open_iddict_scopes",
                table: "OpenIddictScopes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_open_iddict_authorizations",
                table: "OpenIddictAuthorizations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_open_iddict_applications",
                table: "OpenIddictApplications",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_users",
                table: "asp_net_users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_user_tokens",
                table: "asp_net_user_tokens",
                columns: new[] { "user_id", "login_provider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_user_roles",
                table: "asp_net_user_roles",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_user_logins",
                table: "asp_net_user_logins",
                columns: new[] { "login_provider", "provider_key" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_user_claims",
                table: "asp_net_user_claims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_roles",
                table: "asp_net_roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_role_claims",
                table: "asp_net_role_claims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user",
                table: "user",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                table: "asp_net_role_claims",
                column: "role_id",
                principalTable: "asp_net_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_claims_asp_net_users_user_id",
                table: "asp_net_user_claims",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_logins_asp_net_users_user_id",
                table: "asp_net_user_logins",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                table: "asp_net_user_roles",
                column: "role_id",
                principalTable: "asp_net_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_roles_asp_net_users_user_id",
                table: "asp_net_user_roles",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                table: "asp_net_user_tokens",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_open_iddict_authorizations_open_iddict_applications_application",
                table: "OpenIddictAuthorizations",
                column: "application_id",
                principalTable: "OpenIddictApplications",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_open_iddict_tokens_open_iddict_applications_application_id",
                table: "OpenIddictTokens",
                column: "application_id",
                principalTable: "OpenIddictApplications",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_open_iddict_tokens_open_iddict_authorizations_authorization_id",
                table: "OpenIddictTokens",
                column: "authorization_id",
                principalTable: "OpenIddictAuthorizations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_role_role_id",
                table: "user",
                column: "role_id",
                principalTable: "role",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                table: "asp_net_role_claims");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_claims_asp_net_users_user_id",
                table: "asp_net_user_claims");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_logins_asp_net_users_user_id",
                table: "asp_net_user_logins");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_roles_asp_net_users_user_id",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                table: "asp_net_user_tokens");

            migrationBuilder.DropForeignKey(
                name: "fk_open_iddict_authorizations_open_iddict_applications_application",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropForeignKey(
                name: "fk_open_iddict_tokens_open_iddict_applications_application_id",
                table: "OpenIddictTokens");

            migrationBuilder.DropForeignKey(
                name: "fk_open_iddict_tokens_open_iddict_authorizations_authorization_id",
                table: "OpenIddictTokens");

            migrationBuilder.DropForeignKey(
                name: "fk_user_role_role_id",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "pk_role",
                table: "role");

            migrationBuilder.DropPrimaryKey(
                name: "pk_open_iddict_tokens",
                table: "OpenIddictTokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_open_iddict_scopes",
                table: "OpenIddictScopes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_open_iddict_authorizations",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_open_iddict_applications",
                table: "OpenIddictApplications");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_users",
                table: "asp_net_users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_user_tokens",
                table: "asp_net_user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_user_roles",
                table: "asp_net_user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_user_logins",
                table: "asp_net_user_logins");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_user_claims",
                table: "asp_net_user_claims");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_roles",
                table: "asp_net_roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_role_claims",
                table: "asp_net_role_claims");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user",
                table: "user");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "asp_net_users");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "users");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "role",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "role",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "update_date",
                table: "role",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "create_date",
                table: "role",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "OpenIddictTokens",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "subject",
                table: "OpenIddictTokens",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "OpenIddictTokens",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "properties",
                table: "OpenIddictTokens",
                newName: "Properties");

            migrationBuilder.RenameColumn(
                name: "payload",
                table: "OpenIddictTokens",
                newName: "Payload");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OpenIddictTokens",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "reference_id",
                table: "OpenIddictTokens",
                newName: "ReferenceId");

            migrationBuilder.RenameColumn(
                name: "redemption_date",
                table: "OpenIddictTokens",
                newName: "RedemptionDate");

            migrationBuilder.RenameColumn(
                name: "expiration_date",
                table: "OpenIddictTokens",
                newName: "ExpirationDate");

            migrationBuilder.RenameColumn(
                name: "creation_date",
                table: "OpenIddictTokens",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "concurrency_token",
                table: "OpenIddictTokens",
                newName: "ConcurrencyToken");

            migrationBuilder.RenameColumn(
                name: "authorization_id",
                table: "OpenIddictTokens",
                newName: "AuthorizationId");

            migrationBuilder.RenameColumn(
                name: "application_id",
                table: "OpenIddictTokens",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "ix_open_iddict_tokens_reference_id",
                table: "OpenIddictTokens",
                newName: "IX_OpenIddictTokens_ReferenceId");

            migrationBuilder.RenameIndex(
                name: "ix_open_iddict_tokens_authorization_id",
                table: "OpenIddictTokens",
                newName: "IX_OpenIddictTokens_AuthorizationId");

            migrationBuilder.RenameIndex(
                name: "ix_open_iddict_tokens_application_id_status_subject_type",
                table: "OpenIddictTokens",
                newName: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type");

            migrationBuilder.RenameColumn(
                name: "resources",
                table: "OpenIddictScopes",
                newName: "Resources");

            migrationBuilder.RenameColumn(
                name: "properties",
                table: "OpenIddictScopes",
                newName: "Properties");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "OpenIddictScopes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "descriptions",
                table: "OpenIddictScopes",
                newName: "Descriptions");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "OpenIddictScopes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OpenIddictScopes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "display_names",
                table: "OpenIddictScopes",
                newName: "DisplayNames");

            migrationBuilder.RenameColumn(
                name: "display_name",
                table: "OpenIddictScopes",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "concurrency_token",
                table: "OpenIddictScopes",
                newName: "ConcurrencyToken");

            migrationBuilder.RenameIndex(
                name: "ix_open_iddict_scopes_name",
                table: "OpenIddictScopes",
                newName: "IX_OpenIddictScopes_Name");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "OpenIddictAuthorizations",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "subject",
                table: "OpenIddictAuthorizations",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "OpenIddictAuthorizations",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "scopes",
                table: "OpenIddictAuthorizations",
                newName: "Scopes");

            migrationBuilder.RenameColumn(
                name: "properties",
                table: "OpenIddictAuthorizations",
                newName: "Properties");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OpenIddictAuthorizations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "creation_date",
                table: "OpenIddictAuthorizations",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "concurrency_token",
                table: "OpenIddictAuthorizations",
                newName: "ConcurrencyToken");

            migrationBuilder.RenameColumn(
                name: "application_id",
                table: "OpenIddictAuthorizations",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "ix_open_iddict_authorizations_application_id_status_subject_type",
                table: "OpenIddictAuthorizations",
                newName: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type");

            migrationBuilder.RenameColumn(
                name: "settings",
                table: "OpenIddictApplications",
                newName: "Settings");

            migrationBuilder.RenameColumn(
                name: "requirements",
                table: "OpenIddictApplications",
                newName: "Requirements");

            migrationBuilder.RenameColumn(
                name: "properties",
                table: "OpenIddictApplications",
                newName: "Properties");

            migrationBuilder.RenameColumn(
                name: "permissions",
                table: "OpenIddictApplications",
                newName: "Permissions");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OpenIddictApplications",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "redirect_uris",
                table: "OpenIddictApplications",
                newName: "RedirectUris");

            migrationBuilder.RenameColumn(
                name: "post_logout_redirect_uris",
                table: "OpenIddictApplications",
                newName: "PostLogoutRedirectUris");

            migrationBuilder.RenameColumn(
                name: "json_web_key_set",
                table: "OpenIddictApplications",
                newName: "JsonWebKeySet");

            migrationBuilder.RenameColumn(
                name: "display_names",
                table: "OpenIddictApplications",
                newName: "DisplayNames");

            migrationBuilder.RenameColumn(
                name: "display_name",
                table: "OpenIddictApplications",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "consent_type",
                table: "OpenIddictApplications",
                newName: "ConsentType");

            migrationBuilder.RenameColumn(
                name: "concurrency_token",
                table: "OpenIddictApplications",
                newName: "ConcurrencyToken");

            migrationBuilder.RenameColumn(
                name: "client_type",
                table: "OpenIddictApplications",
                newName: "ClientType");

            migrationBuilder.RenameColumn(
                name: "client_secret",
                table: "OpenIddictApplications",
                newName: "ClientSecret");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "OpenIddictApplications",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "application_type",
                table: "OpenIddictApplications",
                newName: "ApplicationType");

            migrationBuilder.RenameIndex(
                name: "ix_open_iddict_applications_client_id",
                table: "OpenIddictApplications",
                newName: "IX_OpenIddictApplications_ClientId");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "asp_net_users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "asp_net_users",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "asp_net_users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "asp_net_users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "two_factor_enabled",
                table: "asp_net_users",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "security_stamp",
                table: "asp_net_users",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "phone_number_confirmed",
                table: "asp_net_users",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "asp_net_users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "asp_net_users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "normalized_user_name",
                table: "asp_net_users",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "normalized_email",
                table: "asp_net_users",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "lockout_end",
                table: "asp_net_users",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "lockout_enabled",
                table: "asp_net_users",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "asp_net_users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "email_confirmed",
                table: "asp_net_users",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "concurrency_stamp",
                table: "asp_net_users",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "access_failed_count",
                table: "asp_net_users",
                newName: "AccessFailedCount");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "asp_net_user_tokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "asp_net_user_tokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "login_provider",
                table: "asp_net_user_tokens",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "asp_net_user_tokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "asp_net_user_roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "asp_net_user_roles",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "asp_net_user_logins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "provider_display_name",
                table: "asp_net_user_logins",
                newName: "ProviderDisplayName");

            migrationBuilder.RenameColumn(
                name: "provider_key",
                table: "asp_net_user_logins",
                newName: "ProviderKey");

            migrationBuilder.RenameColumn(
                name: "login_provider",
                table: "asp_net_user_logins",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "asp_net_user_claims",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "asp_net_user_claims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "claim_value",
                table: "asp_net_user_claims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claim_type",
                table: "asp_net_user_claims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "asp_net_roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "asp_net_roles",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "asp_net_roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "normalized_name",
                table: "asp_net_roles",
                newName: "NormalizedName");

            migrationBuilder.RenameColumn(
                name: "concurrency_stamp",
                table: "asp_net_roles",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "asp_net_role_claims",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "asp_net_role_claims",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "claim_value",
                table: "asp_net_role_claims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claim_type",
                table: "asp_net_role_claims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "update_date",
                table: "users",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "users",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "users",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "create_date",
                table: "users",
                newName: "CreateDate");

            migrationBuilder.RenameIndex(
                name: "ix_user_role_id",
                table: "users",
                newName: "IX_users_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role",
                table: "role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenIddictTokens",
                table: "OpenIddictTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenIddictScopes",
                table: "OpenIddictScopes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenIddictAuthorizations",
                table: "OpenIddictAuthorizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpenIddictApplications",
                table: "OpenIddictApplications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_users",
                table: "asp_net_users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_user_tokens",
                table: "asp_net_user_tokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_user_roles",
                table: "asp_net_user_roles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_user_logins",
                table: "asp_net_user_logins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_user_claims",
                table: "asp_net_user_claims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_roles",
                table: "asp_net_roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_role_claims",
                table: "asp_net_role_claims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_role_claims_asp_net_roles_RoleId",
                table: "asp_net_role_claims",
                column: "RoleId",
                principalTable: "asp_net_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_claims_asp_net_users_UserId",
                table: "asp_net_user_claims",
                column: "UserId",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_logins_asp_net_users_UserId",
                table: "asp_net_user_logins",
                column: "UserId",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_roles_asp_net_roles_RoleId",
                table: "asp_net_user_roles",
                column: "RoleId",
                principalTable: "asp_net_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_roles_asp_net_users_UserId",
                table: "asp_net_user_roles",
                column: "UserId",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_tokens_asp_net_users_UserId",
                table: "asp_net_user_tokens",
                column: "UserId",
                principalTable: "asp_net_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OpenIddictAuthorizations_OpenIddictApplications_Application~",
                table: "OpenIddictAuthorizations",
                column: "ApplicationId",
                principalTable: "OpenIddictApplications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                table: "OpenIddictTokens",
                column: "ApplicationId",
                principalTable: "OpenIddictApplications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                table: "OpenIddictTokens",
                column: "AuthorizationId",
                principalTable: "OpenIddictAuthorizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_role_RoleId",
                table: "users",
                column: "RoleId",
                principalTable: "role",
                principalColumn: "Id");
        }
    }
}
