import { Button, Drawer, Portal, CloseButton, Text } from '@chakra-ui/react';
import { useState } from 'react';
import { useActionData, useLoaderData } from 'react-router';
import FilterForm from './FilterForm.jsx';

export default function Filter() {
    const { maxPrice } = useLoaderData();
    const actionData = useActionData();

    const [filterState, setFilterState] = useState({
        search: "",
        category: "1",
        range: ["0", String(maxPrice)],
        sortBy: "name",
        sortOrder: "asc"
    });

    return (
        <Drawer.Root placement={"left"} size={{ mdDown: "sm", md: "xs" }}>
            <Drawer.Trigger asChild>
                <Button size="sm" ml="4">
                    Фильтр
                </Button>
            </Drawer.Trigger>
            <Portal>
                <Drawer.Backdrop />
                <Drawer.Positioner>
                    <Drawer.Content>
                        <Drawer.Header>
                            <Drawer.Title>
                                <Text fontSize="lg" fontWeight="bold">Фильтр</Text>
                            </Drawer.Title>
                        </Drawer.Header>
                        <Drawer.Body>
                            <FilterForm
                                filterState={filterState}
                                setFilterState={setFilterState}
                            />
                        </Drawer.Body>
                        <Drawer.Footer>
                            <Drawer.ActionTrigger asChild>
                                <Button variant="outline">Отмена</Button>
                            </Drawer.ActionTrigger>
                            <Drawer.ActionTrigger type="submit" asChild>
                                <Button>Подтвердить</Button>
                            </Drawer.ActionTrigger>
                        </Drawer.Footer>
                        <Drawer.CloseTrigger asChild>
                            <CloseButton size="sm" />
                        </Drawer.CloseTrigger>
                    </Drawer.Content>
                </Drawer.Positioner>
            </Portal>
        </Drawer.Root>
    )
}