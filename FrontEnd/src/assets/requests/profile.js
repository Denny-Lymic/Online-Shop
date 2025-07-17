export default async function getProfile() {
    try {
        const token = localStorage.getItem("authToken");
        const response = await fetch("https://localhost:44387/api/Users/profile", {
            method: "GET",
            headers: { "Authorization": `Bearer ${token}` }
        });

        if (!response.ok) {
            console.log("User not found");
        }

        const result = await response.json();
        console.log({ result });

        return result;
    }
    catch {
        console.error("Сервер недоступен");
    }
}