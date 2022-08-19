namespace Assets.Core.Identity.Service.Domain.Constants
{
    /// <summary>
    /// Provide Error Message strings.
    /// </summary>
    public static class ErrorMessage
    {
        /// <summary>
        /// An unknown error has occurred.
        /// </summary>
        public const string UnknownError = "An unknown error was occurred.";

        /// <summary>
        /// An error has occurred while serving your request.
        /// </summary>
        public const string ContactAdmin = "An error has occurred while serving your request. Contact system administrator with correlation id: ";

        /// <summary>
        /// {X} is required.
        /// </summary>
        public const string IsRequired = "{0} is required.";

        /// <summary>
        /// {0} should not be provided, it will be assigned by identity server.
        /// </summary>
        public const string NotRequiredProvidedByIdentityServer = "{0} should not be provided, it will be assigned by identity server.";

        /// <summary>
        /// The client must contain the openid scope.
        /// </summary>
        public const string OpenIdScopeRequired = "The client must contain the openid scope.";

        /// <summary>
        /// A redirect uri is mandatory for the provided grant type.
        /// </summary>
        public const string RedirectUriIsMandatory = "A redirect uri is mandatory for the provided grant type.";

        /// <summary>
        /// One or more redirect uri(s) is/are not well-formed.
        /// </summary>
        public const string RedirectUrisAreNotWellFormed = "One or more redirect uri(s) is/are not well-formed.";

        /// <summary>
        /// A client with Id : {0} already exists.
        /// </summary>
        public const string ClientAlreadyExist = "A client with Id : {0} already exists.";

        /// <summary>
        /// The client with Id : {0} is missing.
        /// </summary>
        public const string ClientMissing = "The client with Id : {0} is missing.";

        /// <summary>
        /// An api-resource with name : {0} already exists.
        /// </summary>
        public const string ApiResourceAlreadyExist = "An api-resource with name : {0} already exists.";

        /// <summary>
        /// An api-resource with the name : {0} is missing.
        /// </summary>
        public const string ApiResourceMissing = "An api-resource with the name : {0} is missing.";

        /// <summary>
        /// An identity resource with the name : {0} already exists.
        /// </summary>
        public const string IdentityResourceAlreadyExist = "An identity resource with the name : {0} already exists.";

        /// <summary>
        /// An Identity Resource with the name : {0} is missing.
        /// </summary>
        public const string IdentityResourceMissing = "An Identity Resource with the name : {0} is missing.";

        /// <summary>
        /// A persisted ticket with key: {0} already exists.
        /// </summary>
        public const string PersistedTicketKeyAlreadyExist = "A persisted ticket with key: {0} already exists.";

        /// <summary>
        /// A persisted ticket with key : {0} is missing.
        /// </summary>
        public const string PersistedTicketKeyNotExist = "A persisted ticket with key : {0} is missing.";

        /// <summary>
        /// Concurrency exception occurred while removing expired tickets: {0}.
        /// </summary>
        public const string PersistedTicketException = "Concurrency exception occurred while removing expired tickets: {0}";

        /// <summary>
        /// School type : {0} is not defined.
        /// </summary>
        public const string UndefinedSchoolType = "School type : {0} is not defined.";

        /// <summary>
        /// {0} claim is missing in subject claims.
        /// </summary>
        public const string ClaimMissingInSubjectClaims = "{0} claim is missing in subject claims.";

        /// <summary>
        /// Person id {0} is not {1}.
        /// </summary>
        public const string InvalidActor = "Person id {0} is not {1}.";

        /// <summary>
        /// Person id {0} is not {1} because of no roles.
        /// </summary>
        public const string NoRoles = "Person id {0} is not {1} because of no roles.";

        /// <summary>
        /// {0} claim has not been received from external provider.
        /// </summary>
        public const string ClaimNotReceivedFromExternalProvider = "{0} claim has not been received from external provider.";

        /// <summary>
        /// An external authentication error has occurred.
        /// </summary>
        public const string ExternalAuthenticationError = "An external authentication error has occurred.";

        /// <summary>
        /// Return url is not valid.
        /// </summary>
        public const string InvalidReturnUrl = "Return url is not valid.";

        /// <summary>
        /// Cors origin call is not allowed for path: {0} from origin: {1}.
        /// </summary>
        public const string InvalidCorsOrigin = "Cors origin call is not allowed for path: {0} from origin: {1}.";

        /// <summary>
        /// Person not found.
        /// </summary>
        public const string PersonNotFound = "Person not found";

        /// <summary>
        /// Professional actor have no roles assigned.
        /// </summary>
        public const string NoRolesAssigned = "No roles assigned to person";

        /// <summary>
        /// Unsupported scope was requested. Scope name used is {0}.
        /// </summary>
        public const string UnsupportedScope = "Unsupported scope was requested. Scope name used is {0}";

        /// <summary>
        /// Invalid {0} domain was requested.
        /// </summary>
        public const string InvalidDomain = "Invalid {0} domain was requested";

        /// <summary>
        /// Invalid person was requested.
        /// </summary>
        public const string InvalidPerson = "Invalid person was requested";

        /// <summary>
        /// X509 certificate does not have a private key.
        /// </summary>
        public const string CertificateThumbprintMissing = "Certificate thumbprint configuration missing.";

        /// <summary>
        /// The certificate file does not exist for thumbprint.
        /// </summary>
        public const string CertificateDoesNotExists = "The certificate does not exist with thumbprint id";

        /// <summary>
        /// X509 certificate does not have a private key.
        /// </summary>
        public const string CertificatePrivateKeyMissing = "X509 certificate does not have a private key.";

        /// <summary>
        /// Signing key is not asymmetric.
        /// </summary>
        public const string CertificateSigningKeyNotValid = "Signing key is not asymmetric.";

        /// <summary>
        /// Artifact is not provided.
        /// </summary>
        public const string ArtifactNotProvided = "Artifact is not provided.";

        /// <summary>
        /// Duplicate scope in identity scopes and Api scopes.
        /// </summary>
        public const string DuplicateScopes = "Found identity scopes and API scopes that use the same names. This is an invalid configuration. Use different names for identity scopes and API scopes. Scopes found: {0}";

        /// <summary>
        /// Failed to activate user {0} in education spaces.
        /// </summary>
        public const string FailedToActivateUser = "Failed to activate user {0} in education spaces.";

        /// <summary>
        /// Failed to activate user {0} in education spaces.
        /// </summary>
        public const string EducationSpacesNotFound = "Education spaces not found when trying to activate user.";
    }
}
