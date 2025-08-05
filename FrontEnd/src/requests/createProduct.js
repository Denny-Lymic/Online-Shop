export async function createProduct({ request }) {
    try {
        const errors = {};

        const formData = await request.formData();

        if (!formData.get("image")) {
            errors.image = "Нет фотографии";
            return { errors, imageBorderColor: "red.500" };
        }

        await fetch(`${import.meta.env.VITE_API_URL}/api/Products/Create`,
            {
                method: "POST",
                body: formData
            })
            .then(res => {
                if (!res.ok) {
                    // const data = res.json();
                    return;
                }
                return res.json();
            })

        return ({ imageBorderColor: "gray.300" })
    }
    catch (e) {
        console.error({ message: e.message || "Сервер недоступен" })
    }
}