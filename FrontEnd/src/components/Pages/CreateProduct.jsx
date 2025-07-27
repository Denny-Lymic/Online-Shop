import NavBar from "../Main/Navigation";
import { Form, useLoaderData } from "react-router";
import ProductForm from "../Main/ProductForm";

export default function CreateProduct() {
    const { categories } = useLoaderData();

    return (
        <Form method="POST" encType="multipart/form-data">
            <NavBar />
            <ProductForm categories={categories}>Добавить продукт</ProductForm>
        </Form>
    )
}

