<script setup lang="ts">
import {onMounted, reactive} from "vue";

interface Driver {
  loading: boolean;
  data?: {
    driver: {
      driverId: string;
      name: string;
      nationality?: string;
    },
    seasonBySeason: {
      year: number;
      races: {
        circuit: string
        name: string
        position: number
        polePosition: boolean
        fastestLap: boolean
      }[]
    }[]
  }
}

const driver = reactive<Driver>({loading: true});

const pointsMap = new Map<number, number>([[1, 25], [2, 18], [3, 15], [4, 12], [5, 10], [6, 8], [7, 6], [8, 4], [9, 2], [10, 1]])

const calculatePoints = (races: { position: number, fastestLap: boolean }[]) =>
    races.reduce((pointTotal, race) => {
      const pointsFromPosition = pointsMap.get(race.position) ?? 0;
      return race.fastestLap && race.position <= 10 ? pointTotal + pointsFromPosition + 1 : pointTotal + pointsFromPosition;
    }, 0)

onMounted(() => {
  fetch('http://localhost:5143/api/Drivers/1')
      .then(response => response.json())
      .then(data => {
        driver.loading = false;
        driver.data = data;
      })
})
</script>

<template>
  <div>
    <h1>Driver</h1>
    <div v-if="driver.loading">Loading...</div>
    <div v-else>
      <div v-if="driver.data">
        <h1>{{ driver.data.driver.name }}</h1>
        <table>
          <tr>
            <th>Year</th>
            <th>Points</th>
            <th>Wins</th>
          </tr>
          <tr v-for="{year, races} in driver.data.seasonBySeason">
            <th scope="row">{{ year }}</th>
            <td>{{ calculatePoints(races) }}</td>
            <td>{{ races.filter(race => race.position === 1).length }}</td>
          </tr>
        </table>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>