export async function updateProduct({ request, params }) {
    try {
        const { id } = params;

        const formData = await request.formData();
        formData.append("id", id);

        await fetch(`/api/Products/Update`,
            {
                method: "PATCH",
                body: formData
            }
        )
            .then(res => {
                if (!res.ok) {
                    // const data = res.json();
                    return;
                }
                return res.json();
            })
    }
    catch (e) {
        return ({ message: e.message || "Сервер недоступен" });
    }
}