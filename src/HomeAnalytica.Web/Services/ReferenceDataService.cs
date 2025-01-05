using HomeAnalytica.Common.DTOs;

namespace HomeAnalytica.Web.Services
{
    /// <summary>
    /// Defines methods for accessing reference data.
    /// </summary>
    public interface IReferenceDataService
    {
        /// <summary>
        /// Retrieves the reference data, including lists of measured quantities and physical units.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a <see cref="ReferenceDataDto"/> result.</returns>
        Task<ReferenceDataDto> GetReferenceDataAsync();
    }

    /// <summary>
    /// Provides methods to retrieve reference data by making HTTP requests.
    /// </summary>
    public class ReferenceDataService : IReferenceDataService
    {
        private readonly ILogger<ReferenceDataService> _logger;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceDataService"/> class.
        /// </summary>
        /// <param name="logger">The logger to use for logging errors.</param>
        /// <param name="httpClient">The HTTP client to make requests.</param>
        public ReferenceDataService(
                                ILogger<ReferenceDataService> logger,
                                HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Retrieves the reference data asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a <see cref="ReferenceDataDto"/> result containing measured quantities and physical units.</returns>
        public async Task<ReferenceDataDto> GetReferenceDataAsync()
        {
            try
            {
                // Sending a GET request to fetch the reference data from the endpoint "/reference-data"
                var referenceData = await _httpClient.GetFromJsonAsync<ReferenceDataDto>("/reference-data");

                // Return the reference data or an empty object if the result is null
                return referenceData ?? new ReferenceDataDto();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
                throw new WebServiceException("Failed to retrieve reference data due to a network issue.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                throw new WebServiceException("An unexpected error occurred.");
            }
        }
    }
}
