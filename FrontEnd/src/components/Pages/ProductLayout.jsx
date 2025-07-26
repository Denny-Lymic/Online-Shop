import { Outlet, useLoaderData } from "react-router";
import NavBar from "../Main/Navigation";

export default function ProductLayout() {
    const { product } = useLoaderData();

    return (
        <>
            <NavBar />
            <Outlet context={product} />
        </>
    );
}