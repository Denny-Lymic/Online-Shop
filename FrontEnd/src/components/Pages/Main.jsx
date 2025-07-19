import NavBar from '../Main/Navigation.jsx'
import { Box, Stack } from '@chakra-ui/react'
import Catalog from '../Main/Catalog/Catalog.jsx'
import Filter from '../Main/Catalog/Filter.jsx'

export default function Main() {
    return (
        <>
            <NavBar></NavBar>
            <Stack className="w-full bg-gray-200" pt="4" pb="10" gap="4" align="start">
                <Filter />
                <Box w="full" display="flex" justifyContent="center">
                    <Catalog />
                </Box>
            </Stack>
        </>
    )
}