import { Await, useOutletContext } from "react-router";
import { Form } from "react-router";
import ProductForm from "../Main/ProductForm";
import { Suspense } from "react";
import viteLogo from "/vite.svg"

export default function UpdateProduct() {
    const { categories, product } = useOutletContext();
    const IMAGE_BASE_URL = import.meta.env.VITE_API_URL || '';

    return (
        <Suspense>
            <Await resolve={Promise.all([product, categories])}>
                {([product, categories]) => (
                    <Form method="POST" encType="multipart/form-data">
                        <ProductForm
                            categories={categories}
                            defaultName={product.name}
                            defaultPrice={product.price}
                            defaultCategory={product.category}
                            defaultImageSrc={product.imageUrl ? `${IMAGE_BASE_URL}/images/products/${product.imageUrl}` : viteLogo}
                            defaultDescription={product.description}
                        >
                            Изменить продукт
                        </ProductForm>
                    </Form>
                )}
            </Await>
        </Suspense>
    );
}