import axios from "axios";

let instance = axios.create({
    baseURL: "https://fitness-tracker.germanywestcentral.cloudapp.azure.com/api/",
    responseType: "json",
});

instance.interceptors.request.use((request) => {
    const token = localStorage.getItem("token");
    if (token) {
        request.headers = {...request.headers, Authorization: `Bearer ${token}`};
    }
    return request;
});
instance.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        if ([401, 403].includes(error.response.status)) {
            localStorage.removeItem("token");
            // eslint-disable-next-line no-restricted-globals
            location.replace("/login");
        }
        return error;
    }
);
export default instance;
