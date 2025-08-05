import { Grid, Skeleton, Box } from "@chakra-ui/react"
import { CardEntity } from "./Card"
import viteLogo from '/vite.svg'

export default function SkeletonLoad({ columns }) {
    return (
        <Grid templateColumns={`repeat(${columns}, 1fr)`} gap={2} w="5/6" h="auto">
            {Array(columns * 2).fill(0).map((_, index) => (
                <Skeleton
                    key={index}
                    variant="shine"
                    css={{
                        "--start-color": "colors.gray.200",
                        "--end-color": "colors.gray.300",
                    }}
                    width="full"
                    height="auto"
                    minH="250px"
                >
                    <CardEntity
                        key={index}
                        imageSrc={viteLogo}
                        title="TitleTiasdasd"
                        price="Price"
                    />
                </Skeleton>
            ))}
        </Grid>
    )
}
