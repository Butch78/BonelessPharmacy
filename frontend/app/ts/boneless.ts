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
    public static readonly GetToken = (): string => {
        return localStorage.getItem('boneless-token');
    }

    /**
     * Add the token to the localstorage
     */
    public static readonly SetToken = (token) => {
        localStorage.setItem('boneless-token', token);
        Boneless.loggedIn = true;
        console.log(token);
        return token;
    }

    /**
     * Retrieve or update the logged in user of Boneless pharmacy
     */
    public static readonly Login = (staff: Staff = null): Staff => {
        let res: Staff;
        if (staff !== null) {
            localStorage.setItem('boneless-user', JSON.stringify(staff));
            res = staff;
        } else {
            res = localStorage.getItem('boneless-user') !== null
                ? JSON.parse(localStorage.getItem('boneless-user')) as Staff : null;
        }
        if (res != null) {
            let oldStaffBtn = document.getElementById("staffTitle");
            let staffBtn = document.getElementById("dropDown");
            staffBtn.innerText = res.name;
            oldStaffBtn.style.display = 'none';
            staffBtn.style.display = 'inline-block';
            staffBtn.setAttribute("href", "#!"); // Maybe this can be imporved??
        }
        return res;
    }

    /**
     * Logout of the application
     */
    public static readonly Logout = () => {
        localStorage.removeItem('boneless-user');
        localStorage.removeItem('boneless-token');
        window.location.reload();
    }

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

    /**
     * Getter for logged in status
     */
    public static readonly IsLoggedIn = () => Boneless.Login() !== null;

    /**
     * Generate a new URL based file for text
     */
    public static readonly CreateFile =
        (value: string,
         filename: string = "report.csv",
         type: string = "text/plain") => {
            let data = new Blob([value], {type});
            if (Boneless.currentFile !== null) {
                window.URL.revokeObjectURL(Boneless.currentFile);
            }
            Boneless.currentFile = window.URL.createObjectURL(data);
            return Boneless.currentFile;
        }

    private static currentFile = null;
    /**
     * Used to define whether a user has attempted a login
     */
    private static loggedIn = false;

}

/**
 * Standard status messages to be used in the application
 */
enum BonelessStatusMessage {
    INVALID_GET = "Error retrieving data, please ensure you are connected to the internet.",
    INVALID_POST = "Error processing data, please ensure fields are correct and you are connected to internet.",
    INVALID_REPORT = "Error processing report, please ensure you are connected to internet.",
}
