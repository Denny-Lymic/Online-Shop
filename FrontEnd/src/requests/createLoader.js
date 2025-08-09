export async function loader() {
    try {
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
            categories: await categoriesPromise,
        })
    }
    catch (e) {
        console.error({ message: e.message || "Сервер недоступен" });
    }
}