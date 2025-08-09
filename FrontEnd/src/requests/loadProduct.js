export async function loader({ params }) {
    try {
        const { id } = params;

        const userPromise = fetch(`/api/Users/profile`, {
            method: "GET",
            headers: { "Authorization": `Bearer ${localStorage.getItem("authToken")}` }
        })
            .then(res => {
                if (!res.ok) {
                    return
                }
                return res.json()
            });

        const productPromise = fetch(`/api/Products/${id}`, {
            method: "GET",
        })
            .then(res => {
                if (!res.ok) {
                    return
                }
                return res.json();
            })

        const categoriesPromise = fetch(`/api/Products/Categories`, {
            method: "GET"
        })
            .then(res => {
                if (!res.ok) {
                    return;
                }
                return res.json();
            });

        return ({
            user: Promise.resolve(userPromise),
            product: Promise.resolve(productPromise),
            categories: Promise.resolve(categoriesPromise),
        });
    }
    catch (e) {
        console.log({ message: e.message || "Сервер недоступен" });
    }
}