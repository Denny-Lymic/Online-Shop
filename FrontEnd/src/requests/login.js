export async function action({ request }) {
    const errors = {};

    try {
        const formData = await request.formData();
        const email = formData.get("email");
        const password = formData.get("password");

        const errors = {};
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (!email || typeof email !== "string" || !emailPattern.test(email))
            errors.email = "Неправильный email";

        if (!password || password.length < 4)
            errors.password = "Слишком короткий пароль"

        if (Object.keys(errors).length)
            return { errors };

        const res = await fetch(`${import.meta.env.VITE_API_URL}/api/Users/Login`, {
            method: "POST",
            body: JSON.stringify({ email, password }),
            headers: { "Content-Type": "application/json" }
        });

        if (!res.ok) {
            errors.invalidData = "Неверные данные";
            return { errors }
        }

        const result = await res.json();

        localStorage.setItem("authToken", result.token);

        return { token: result.token };
    }
    catch {
        console.error("Сервер недоступен");
        errors.server = "Сервер недоступен, попробуйте позже :("
        return { errors }
    }
}