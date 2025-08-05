export async function getProducts({ request }) {
    try {
        const formData = await request.formData();

        const filter = Array.from(formData.entries()).filter(([, value]) => value !== "" && value !== "0");

        const searchParams = new URLSearchParams(filter).toString();

        const searchPromise = fetch(`${import.meta.env.VITE_API_URL}/api/Products/Search?${searchParams}`, {
            method: "GET"
        })
            .then(res => {
                if (!res.ok) {
                    throw new Error("Failed to fetch products");
                }
                return res.json();
            });

        return {
            products: Promise.resolve(searchPromise)
        };
    }
    catch (error) {
        console.error("Error in getProducts:", error);
    }
}
