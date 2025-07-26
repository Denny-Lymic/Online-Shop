export async function loader({ params }) {
    try {
        const { id } = params;

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

        const productPromise = fetch(`${import.meta.env.VITE_API_URL}/api/Products/${id}`, {
            method: "GET",
        })
            .then(res => {
                if (!res.ok) {
                    return
                }
                return res.json();
            })

        return ({
            user: Promise.resolve(userPromise),
            product: Promise.resolve(productPromise),
        });
    }
    catch (e) {
        console.log({ message: e.message || "Сервер недоступен" });
    }
}