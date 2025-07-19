export async function loader() {
    try {
        const userPromise = fetch(`${import.meta.env.VITE_API_URL}/api/Users/profile`, {
            method: "GET",
            headers: { "Authorization": `Bearer ${localStorage.getItem("authToken")}` }
        })
            .then(res => {
                if (!res.ok) {
                    return
                }
                return res.json()
            });

        const productsPromise = fetch(`${import.meta.env.VITE_API_URL}/api/Products`, {
            method: "GET"
        })
            .then(res => {
                if (!res.ok) {
                    return;
                }
                return res.json();
            });

        return {
            user: Promise.resolve(userPromise),
            products: Promise.resolve(productsPromise)
        };
    }
    catch (e) {
        console.error({ message: e.message || "Свервер недоступен" });
    }
}

