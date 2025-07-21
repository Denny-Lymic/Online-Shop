export async function getProducts({ request }) {
    const formData = await request.formData();

    const filter = Array.from(formData.entries());
    console.log("Filter data:", filter);

    // try {
    //     const response = await fetch(`${import.meta.env.VITE_API_URL}/api/Products`, {
    //         method: "GET"
    //     });

    //     if (!response.ok) {
    //         throw new Error("Failed to fetch products");
    //     }

    //     const products = await response.json();
    //     return products;
    // } catch (error) {
    //     console.error("Error fetching products:", error);
    //     throw error; // Re-throw the error for further handling
    // }
}

function objToQueryString(obj) {
    return Object.entries(obj)
        .map(([key, value]) => `${encodeURIComponent(key)}=${encodeURIComponent(value)}`)
        .join('&');
}
