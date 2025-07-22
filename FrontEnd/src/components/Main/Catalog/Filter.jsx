import { Button, Drawer, Portal, CloseButton, Text } from '@chakra-ui/react';
import { useState } from 'react';
import { Form, useLoaderData } from 'react-router';
import FilterForm from './FilterForm.jsx';

export default function Filter() {
    const { maxPrice } = useLoaderData();

    const [filterState, setFilterState] = useState({
        search: "",
        category: "name",
        range: ["0", String(maxPrice)],
        sortBy: "name",
        sortOrder: "asc"
    });

    const [sliderRange, setSliderRangeStr] = useState(["0", String(maxPrice)]);

    return (
        <Drawer.Root placement={"left"} size={{ mdDown: "sm", md: "xs" }}>
            <Drawer.Trigger asChild>
                <Button size="sm" ml="4">
                    Фильтр
                </Button>
            </Drawer.Trigger>

            <Portal>
                <Form method="POST">
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
                                    sliderRangeStr={sliderRange}
                                    setSliderRangeStr={setSliderRangeStr}
                                />
                            </Drawer.Body>

                            <Drawer.Footer>
                                <Drawer.ActionTrigger asChild>
                                    <Button variant="outline">Отмена</Button>
                                </Drawer.ActionTrigger>
                                <Drawer.ActionTrigger asChild>
                                    <Button type="submit">Подтвердить</Button>
                                </Drawer.ActionTrigger>
                            </Drawer.Footer>

                            <Drawer.CloseTrigger asChild>
                                <CloseButton size="sm" />
                            </Drawer.CloseTrigger>

                        </Drawer.Content>
                    </Drawer.Positioner>
                </Form>
            </Portal>

        </Drawer.Root >
    )
}