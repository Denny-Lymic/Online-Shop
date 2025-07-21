import { Await, useActionData, useLoaderData } from 'react-router'
import { CardEntity } from './Card.jsx'
import viteLogo from '/vite.svg'
import { Box, Grid, Skeleton, useBreakpointValue } from '@chakra-ui/react'
import { Suspense } from 'react'

export default function Catalog() {
    const { products: initialProducts } = useLoaderData();
    const actionData = useActionData();
    const products = actionData?.products || initialProducts;

    const columns = useBreakpointValue({ base: 2, sm: 3, md: 5, lg: 7 });

    return (
        <Suspense fallback={
            <Grid templateColumns={`repeat(${columns}, 1fr)`} gap={2} w="5/6" h="auto">
                {Array(columns).fill(0).map((_, index) => (
                    <Skeleton
                        key={index}
                        variant="shine"
                        css={{
                            "--start-color": "colors.gray.200",
                            "--end-color": "colors.gray.300",
                        }}>
                        <CardEntity
                            key={index}
                            imageSrc={viteLogo}
                            title="TitleTitle"
                            price="Price"
                        />
                    </Skeleton>
                ))}
            </Grid>
        }>
            <Await resolve={products}>
                {(data) => (
                    <Grid templateColumns={`repeat(${columns}, 1fr)`} gap={2} w="5/6" h="auto">
                        {data.map(product => (
                            <CardEntity
                                key={product.id}
                                imageSrc={viteLogo}
                                title={product.name}
                                price={product.price}
                            />
                        ))}
                    </Grid>
                )}
            </Await>
        </Suspense >


    )
}