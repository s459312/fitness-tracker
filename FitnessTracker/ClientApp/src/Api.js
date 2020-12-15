import axios from "axios";

let instance = axios.create({
  baseURL: "http://localhost:5001/api/",
  responseType: "json",
  headers: {
    Authorization: `Bearer ${localStorage.getItem("token") ?? ""}`,
  },
});
instance.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (![401, 403].includes(error.response.status)) {
      localStorage.removeItem("token");
      // eslint-disable-next-line no-restricted-globals
      location.replace('/login')
    }
    return error;
  }
);
export default instance;
