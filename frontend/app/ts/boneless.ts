declare const Papa: any;
/**
 * Singleton helper for the application
 */
class Boneless {
    /**
     * The current endpoint to be querying with a '/' appended
     */
    public static readonly API_ENDPOINT: string = "http://localhost:5000/";

    /**
     * Create a request object with the necessary token auth
     */
    public static CreateRequest = (endpoint: string, method: string, data = {}) => {
        return {
            data,
            headers: {
                Authorization: `Bearer ${Boneless.GetToken()}`,
            },
            method,
            url: `${Boneless.API_ENDPOINT}${endpoint}`,
        };
    }

    /**
     * Retrieve the token for the current user
     * @todo actually implement the token shit
     */
    public static readonly GetToken = () => (
        // tslint:disable-next-line:max-line-length
        'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI3ZGY4OTYwNy05OTE1LTQ5MjAtOTA0YS1hZTk2MzBmYTA5Y2YiLCJleHAiOjE1MDUxMTY5NTUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.3ZXAE6Bnq76-LiCvw_VObPRBO7SD-ri3pfFeQybd2Xo'
    )

    /**
     * Easily show a standard toast alert using a custom message
     */
    public static readonly NotifyCustom = (message: string) => Materialize.toast(message, 4000);

    /**
     * Easily show a standard toast alert using a pre-defined message
     */
    public static readonly Notify = (type: BonelessStatusMessage) => Materialize.toast(type, 4000);

    /**
     * Parse the CSV string to an object
     */
    public static readonly ParseCsv = (input: string) => Papa.parse(input).data;
}

/**
 * Standard status messages to be used in the application
 */
enum BonelessStatusMessage {
    INVALID_GET = "Error retrieving data, please ensure you are connected to the internet.",
    INVALID_POST = "Error processing data, please ensure fields are correct and you are connected to internet.",
}
