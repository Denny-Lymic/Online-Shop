export async function action({ request }) {
    const errors = {};

    try {
        const formData = await request.formData();
        const email = formData.get("email");
        const userName = formData.get("userName");
        const password = formData.get("password");
        const passwordCheck = formData.get("passwordCheck");

        const errors = {};
        const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (!email || typeof email !== "string" || !emailPattern.test(email))
            errors.email = "Неправильный email";

        if (!userName || typeof email !== "string")
            errors.email = "Введите коректное имя";

        if (!password || password.length < 4)
            errors.password = "Слишком короткий пароль"
        else if (password !== passwordCheck)
            errors.password = "Пароли не совпадают"

        if (Object.keys(errors).length) {
            return { errors, color: "red.200" };
        }

        const res = await fetch(`/api/Users`, {
            method: "POST",
            body: JSON.stringify({ Email: email, Name: userName, Password: password }),
            headers: { "Content-Type": "application/json" }
        });

        if (!res.ok) {
            const data = await res.json();
            console.log(data);
            return { errors: { server: data.errors[0] }, color: "red.200" }
        }

        return { success: { message: "Регистрация успешна" }, color: "green.200" }
    }
    catch (e) {
        console.error("Сервер недоступен: " + e);
        errors.server = "Сервер недоступен, попробуйте позже :("
        return { errors, color: "red.200" }
    }
}