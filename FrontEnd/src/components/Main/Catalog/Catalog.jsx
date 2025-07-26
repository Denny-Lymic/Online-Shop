import { Await, Link, useActionData, useLoaderData, useNavigation } from 'react-router'
import { CardEntity } from './Card.jsx'
import viteLogo from '/vite.svg'
import { Grid, LinkBox, LinkOverlay, useBreakpointValue } from '@chakra-ui/react'
import { Suspense } from 'react'
import SkeletonLoad from './Skeleton.jsx'

export default function Catalog() {
    const { products: initialProducts } = useLoaderData();
    const actionData = useActionData();
    const navigation = useNavigation();
    const isBusy = navigation.state !== "idle";
    const products = actionData?.products || initialProducts;

    const columns = useBreakpointValue({ base: 2, sm: 3, md: 5, lg: 6 });

    return (
        <>
            {isBusy && (
                <SkeletonLoad columns={columns} />
            )}
            <Suspense fallback={<SkeletonLoad columns={columns} />}>
                <Await resolve={products}>
                    {(data) => (
                        <Grid templateColumns={`repeat(${columns}, 1fr)`} gap={2} w="5/6" h="auto">
                            {data.map(product => (
                                <LinkBox key={product.id}>
                                    <LinkOverlay as={Link} to={`product/${product.id}`}>
                                        <CardEntity

                                            imageSrc={product.imageUrl ? `${import.meta.env.VITE_API_URL}/images/products/${product.imageUrl}` : viteLogo}
                                            title={product.name}
                                            price={product.price}
                                        />
                                    </LinkOverlay>
                                </LinkBox>
                            ))}
                        </Grid>
                    )}
                </Await>
            </Suspense >
        </>
    )
}