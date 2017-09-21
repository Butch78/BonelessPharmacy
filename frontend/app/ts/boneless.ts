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
    public static CreateRequest = (endpoint: string, method: string) => {
        return {
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
}