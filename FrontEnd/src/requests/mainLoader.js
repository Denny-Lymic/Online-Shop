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

        const categoriesPromise = fetch(`${import.meta.env.VITE_API_URL}/api/Products/Categories`, {
            method: "GET"
        })
            .then(res => {
                if (!res.ok) {
                    return;
                }
                return res.json();
            });

        const maxPricePromise = fetch(`${import.meta.env.VITE_API_URL}/api/Products/MaxPrice`, {
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
            products: Promise.resolve(productsPromise),
            categories: await Promise.resolve(categoriesPromise),
            maxPrice: await Promise.resolve(maxPricePromise)
        };
    }
    catch (e) {
        console.error({ message: e.message || "Свервер недоступен" });
    }
}

