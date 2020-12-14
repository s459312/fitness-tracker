import axios from "axios";

export default axios.create({
    baseURL: "http://localhost:5001/api/",
    responseType: "json",
    headers: {
        Authorization: `Bearer ${localStorage.getItem('token') ?? ''}`
    }
});